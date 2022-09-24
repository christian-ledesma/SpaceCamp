import { ChangeEvent, useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/Activity";

interface Props {
    selectedActivity: Activity | undefined;
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
    submitting: boolean;
}


export const ActivityForm = ({ selectedActivity, closeForm, createOrEdit, submitting }: Props) => {
    const initialState = selectedActivity ?? {
        id: "",
        name: "",
        category: "",
        description: "",
        date: "",
        city: "",
        venue: ""
    };

    const [activity, setActivity] = useState(initialState);

    const handleSubmit = () => {
        createOrEdit(activity);
    };

    const handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target
        setActivity({ ...activity, [name]: value })
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete="off">
                <Form.Input placeholder="Name" value={activity.name} name="name" onChange={handleInputChange} />
                <Form.TextArea placeholder="Description" value={activity.description} name="description" onChange={handleInputChange} />
                <Form.Input placeholder="Category" value={activity.category} name="category" onChange={handleInputChange} />
                <Form.Input placeholder="Date" type="date" value={activity.date} name="date" onChange={handleInputChange} />
                <Form.Input placeholder="City" value={activity.city} name="city" onChange={handleInputChange} />
                <Form.Input placeholder="Venue" value={activity.venue} name="venue" onChange={handleInputChange} />
                <Button loading={submitting} floated="right" positive type="submit" content="Submit" />
                <Button floated="right" type="button" content="Cancel" onClick={closeForm} />
            </Form>
        </Segment>
    )
};