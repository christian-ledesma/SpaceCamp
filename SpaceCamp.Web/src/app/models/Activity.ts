import { Profile } from "./Profile";

export interface Activity {
    id: string;
    name: string;
    date: Date | null;
    description: string;
    category: string;
    city: string;
    venue: string;
    hostUsername: string;
    isCancelled: boolean;
    isGoing: boolean;
    isHost: boolean;
    host?: Profile;
    attendees: Profile[]
}

export class Activity implements Activity {
    constructor(init?: ActivityFormValues) {
        Object.assign(this, init);
    }
}

export class ActivityFormValues {
    id?: string = undefined;
    name: string = '';
    category: string = '';
    description: string = '';
    date: Date | null = null;
    city: string = '';
    venue: string = '';

    constructor(activity?: ActivityFormValues) {
        if (activity) {
            this.id = activity.id;
            this.name = activity.name;
            this.description = activity.description;
            this.category = activity.category;
            this.date = activity.date;
            this.city = activity.city;
            this.venue = activity.venue;
        }
    }
}
