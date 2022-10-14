import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Grid } from "semantic-ui-react"
import { Loading } from "../../../app/layout/Loading";
import { useStore } from "../../../app/stores/store";
import { ActivityFilters } from "./ActivityFilters";
import { ActivityList } from "./ActivityList";


export const ActivityDashboard = observer(() => {
    const { activityStore } = useStore();
    const { loadActivities, activityList } = activityStore;

    useEffect(() => {
        if (activityList.size <= 1) loadActivities();
    }, [activityList.size, loadActivities]);

    if (activityStore.loadingInitial) return (
        <Loading />
    );

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityFilters />
            </Grid.Column>
        </Grid>
    );
});
