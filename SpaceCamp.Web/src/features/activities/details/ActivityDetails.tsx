import { Button, Card, Image } from "semantic-ui-react";
import { Activity } from "../../../app/models/Activity";

interface Props {
    activity: Activity;
    cancelSelectActivity: () => void;
    openForm: (id: string) => void;
}


export const ActivityDetails = ({ activity, cancelSelectActivity, openForm }: Props) => {
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
                    <Button basic color="blue" content="edit" onClick={() => openForm(activity.id)} />
                    <Button basic color="grey" content="cancel" onClick={cancelSelectActivity} />
                </Button.Group>
            </Card.Content>
        </Card>
    );
};