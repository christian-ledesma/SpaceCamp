import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Button, Header, Segment } from "semantic-ui-react";
import { Loading } from "../../../app/layout/Loading";
import { useStore } from "../../../app/stores/store";
import { v4 as uuid } from 'uuid';
import { Link } from "react-router-dom";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import { CustomTextInput } from "../../../app/common/form/CustomTextInput";
import { CustomTextArea } from "../../../app/common/form/CustomTextArea";
import { CustomSelectInput } from "../../../app/common/form/CustomSelectInput";
import { CategoryOptions } from "../../../app/common/options/CategoryOptions";
import { CustomDateInput } from "../../../app/common/form/CustomDateInput";
import { Activity } from "../../../app/models/Activity";

export const ActivityForm = observer(() => {
    const history = useHistory();
    const { activityStore } = useStore();
    const { createActivity, updateActivity, loading, loadActivity, loadingInitial } = activityStore;
    const { id } = useParams<{ id: string }>();

    const [activity, setActivity] = useState<Activity>({
        id: "",
        name: "",
        category: "",
        description: "",
        date: null,
        city: "",
        venue: ""
    });

    const validationSchema = Yup.object({
        name: Yup.string().required("The name is required"),
        description: Yup.string().required("The description is required"),
        category: Yup.string().required("The category is required"),
        date: Yup.string().required("The date is required").nullable(),
        city: Yup.string().required("The city is required"),
        venue: Yup.string().required("The venue is required"),
    })

    useEffect(() => {
        if (id) loadActivity(id).then(a => setActivity(a!));
    }, [id, loadActivity]);

    const submitHandler = (activity: Activity) => {
        if (activity.id.length === 0) {
            let newActivity = {
                ...activity,
                id: uuid()
            };
            createActivity(newActivity).then(() => {
                history.push(`/activities/${newActivity.id}`);
            });
        } else {
            updateActivity(activity).then(() => {
                history.push(`/activities/${activity.id}`);
            });
        }
    };

    if (loadingInitial) return <Loading />;

    return (
        <Segment clearing>
            <Header content="Activity details" sub color="teal" />
            <Formik
                validationSchema={validationSchema}
                enableReinitialize
                initialValues={activity}
                onSubmit={values => submitHandler(values)}>
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <CustomTextInput name="name" placeholder="Name" />
                        <CustomTextArea placeholder="Description" name="description" rows={3} />
                        <CustomSelectInput options={CategoryOptions} placeholder="Category" name="category" />
                        <CustomDateInput
                            placeholderText="Date"
                            name="date"
                            showTimeSelect
                            timeCaption="time"
                            dateFormat="MMMM d, yyyy h:mm aa" />
                        <Header content="Location details" sub color="teal" />
                        <CustomTextInput placeholder="City" name="city" />
                        <CustomTextInput placeholder="Venue" name="venue" />
                        <Button
                            loading={loading}
                            floated="right"
                            disabled={isSubmitting || !isValid || !dirty}
                            positive type="submit"
                            content="Submit" />
                        <Button as={Link} to="/activities" floated="right" type="button" content="Cancel" />
                    </Form>
                )}
            </Formik>
        </Segment>
    )
});
