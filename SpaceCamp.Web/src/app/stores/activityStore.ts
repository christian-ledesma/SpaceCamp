import { format } from "date-fns";
import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/Activity";
import agent from "../services/agent";

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
        activity.date = new Date(activity.date!);
        this.activityList.set(activity.id, activity);
    };

    private getActivityInMemory = (id: string) => {
        return this.activityList.get(id);
    };

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    };

    createActivity = async (activity: Activity) => {
        this.loading = true;
        try {
            await agent.Activities.create(activity);
            runInAction(() => {
                this.activityList.set(activity.id, activity);
                this.selectedActivity = activity;
                this.editMode = false;
                this.loading = false;
            });
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };

    updateActivity = async (activity: Activity) => {
        this.loading = true;
        try {
            await agent.Activities.update(activity);
            runInAction(() => {
                this.activityList.set(activity.id, activity);
                this.selectedActivity = activity;
                this.editMode = false;
                this.loading = false;
            });
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            });
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
}
