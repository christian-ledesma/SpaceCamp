import { observer } from "mobx-react-lite";
import { Button, Card, Image } from "semantic-ui-react";
import { Loading } from "../../../app/layout/Loading";
import { useStore } from "../../../app/stores/store";


export const ActivityDetails = observer(() => {
    const { activityStore } = useStore();
    const { selectedActivity: activity, openForm, cancelSelectedActivity } = activityStore;

    if (!activity) return <Loading />;

    return (
        <Card fluid>
            <Image src={`/assets/categoryImages/${activity.category}.jpg`} />
            <Card.Content>
                <Card.Header>{activity.name}</Card.Header>
                <Card.Meta>
                    <span>{activity.date}</span>
                </Card.Meta>
                <Card.Description>
                    {activity.description}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Button.Group widths={2}>
                    <Button basic color="blue" content="Edit" onClick={() => openForm(activity.id)} />
                    <Button basic color="grey" content="Cancel" onClick={cancelSelectedActivity} />
                </Button.Group>
            </Card.Content>
        </Card>
    );
});
