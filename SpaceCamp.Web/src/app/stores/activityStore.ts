import { format } from "date-fns";
import { makeAutoObservable, runInAction } from "mobx";
import { Activity, ActivityFormValues } from "../models/Activity";
import { Profile } from "../models/Profile";
import agent from "../services/agent";
import { store } from "./store";

export default class ActivityStore {
    activityList = new Map<string, Activity>();
    selectedActivity: Activity | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = false;

    constructor() {
        makeAutoObservable(this)
    }

    get ActivitiesByDate() {
        return Array.from(this.activityList.values())
            .sort((a, b) => a.date!.getTime() - b.date!.getTime());
    };

    get groupedActivities() {
        return Object.entries(
            this.ActivitiesByDate.reduce((activities, activity) => {
                const date = format(activity.date!, 'dd MMM yyyy');
                activities[date] = activities[date] ? [...activities[date], activity] : [activity]
                return activities;
            }, {} as {[key: string]: Activity[]})
        );
    };

    loadActivities = async () => {
        this.setLoadingInitial(true);
        try {
            const activities = await agent.Activities.list();
            activities.forEach(a => {
                this.setActivity(a);
            });
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false);
        }
    };

    loadActivity = async (id: string) => {;
        this.setLoadingInitial(true);
        let _activity = this.getActivityInMemory(id);
        if (_activity) {
            this.selectedActivity = _activity;
            this.setLoadingInitial(false);
            return _activity;
        } else {
            try {
                _activity = await agent.Activities.details(id);
                this.setActivity(_activity);
                runInAction(() => {
                    this.selectedActivity = _activity;
                });
                this.setLoadingInitial(false);
                return _activity;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    };

    private setActivity = (activity: Activity) => {
        const user = store.userStore.user;
        if (user) {
            activity.isGoing = activity.attendees!.some(x => x.username === user.username);
            activity.isHost = activity.hostUsername === user.username;
            activity.host = activity.attendees?.find(x => x.username === activity.hostUsername);
        }
        activity.date = new Date(activity.date!);
        this.activityList.set(activity.id, activity);
    };

    private getActivityInMemory = (id: string) => {
        return this.activityList.get(id);
    };

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    };

    createActivity = async (activity: ActivityFormValues) => {
        const user = store.userStore.user;
        const attendees = new Profile(user!);
        try {
            await agent.Activities.create(activity);
            const _activity = new Activity(activity);
            _activity.hostUsername = user!.username;
            _activity.attendees = [attendees];
            this.setActivity(_activity);
            runInAction(() => {
                this.selectedActivity = _activity;
            });
        } catch (error) {
            console.log(error);
        }
    };

    updateActivity = async (activity: ActivityFormValues) => {
        try {
            await agent.Activities.update(activity);
            runInAction(() => {
                if (activity.id) {
                    let _activity = {...this.getActivityInMemory(activity.id), ...activity};
                    this.activityList.set(activity.id, _activity as Activity);
                    this.selectedActivity = _activity as Activity;
                }
            });
        } catch (error) {
            console.log(error);
        }
    };

    deleteActivity = async (id: string) => {
        this.loading = true;
        try {
            await agent.Activities.delete(id);
            runInAction(() => {
                this.activityList.delete(id);
                this.loading = false;
            });
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };

    updateAttendance = async () => {
        const user = store.userStore.user;
        this.loading = true;
        try {
            await agent.Activities.attend(this.selectedActivity!.id);
            runInAction(() => {
                if (this.selectedActivity?.isGoing) {
                    this.selectedActivity.attendees = this.selectedActivity.attendees?.filter(x => x.username !== user?.username);
                    this.selectedActivity.isGoing = false;
                } else {
                    const attendee = new Profile(user!);
                    this.selectedActivity?.attendees?.push(attendee);
                    this.selectedActivity!.isGoing = true;
                }

                this.activityList.set(this.selectedActivity!.id, this.selectedActivity!)
            });
        } catch(error) {

        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    };

    cancelActivityToggle = async() => {
        this.loading = true;
        try {
            await agent.Activities.attend(this.selectedActivity!.id);
            runInAction(() => {
                this.selectedActivity!.isCancelled = !this.selectedActivity?.isCancelled;
                this.activityList.set(this.selectedActivity!.id, this.selectedActivity!)
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    };
}
