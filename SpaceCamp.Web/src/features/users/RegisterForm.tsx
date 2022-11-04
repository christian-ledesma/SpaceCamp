import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Header } from "semantic-ui-react";
import { CustomTextInput } from "../../app/common/form/CustomTextInput";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import { ValidationErrors } from "../errors/ValidationErrors";


export const RegisterForm = observer(() => {
    const { userStore } = useStore();
    return (
        <Formik
            initialValues={{ displayName: '', username: '', email: '', password: '', error: null }}
            onSubmit={(values, { setErrors }) => userStore.register(values)
                .catch(error => setErrors({ error }))}
            validationSchema={Yup.object({
                displayName: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required(),
            })}>
            {({ handleSubmit, isSubmitting, errors, isValid, dirty }) => (
                <Form className="ui form error" onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Signup to SpaceCamp' color='teal' textAlign="center" />
                    <CustomTextInput name="displayName" placeholder="Display Name" />
                    <CustomTextInput name="username" placeholder="Username" />
                    <CustomTextInput name="email" placeholder="Email" />
                    <CustomTextInput name="password" placeholder="Password" type="password" />
                    <ErrorMessage name="error" render={() => <ValidationErrors errors={errors.error} />} />
                    <Button loading={isSubmitting} positive content='Register' type="submit" fluid disabled={!isValid || !dirty || isSubmitting} />
                </Form>
            )}
        </Formik>
    );
});
