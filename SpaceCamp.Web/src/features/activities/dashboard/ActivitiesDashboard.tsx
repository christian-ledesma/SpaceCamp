import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react"
import { Activity } from "../../../app/models/Activity"
import { useStore } from "../../../app/stores/store";
import { ActivityDetails } from "../details/ActivityDetails";
import { ActivityForm } from "../form/ActivityForm";
import { ActivityList } from "./ActivityList";


interface Props {
    activities: Activity[];
    deleteActivity: (id: string) => void;
    submitting: boolean;
}

export const ActivityDashboard = observer((props: Props) => {
    const { activityStore } = useStore();
    const { selectedActivity, editMode } = activityStore;

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityList
                    submitting={props.submitting}
                    activities={props.activities}
                    deleteActivity={props.deleteActivity} />
            </Grid.Column>
            <Grid.Column width={6}>
                {selectedActivity && !editMode &&
                    <ActivityDetails />}
                {editMode &&
                    <ActivityForm />}
            </Grid.Column>
        </Grid>
    );
});
