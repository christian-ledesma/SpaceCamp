import { Button, Container, Menu } from "semantic-ui-react";
import { useStore } from "../stores/store";

export const NavBar = () => {
    const { activityStore } = useStore();

    return (
        <Menu inverted fixed="top">
            <Container>
                <Menu.Item header>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: "5px" }} />
                    SpaceCamp
                </Menu.Item>
                <Menu.Item name="Activities">
                </Menu.Item>
                <Menu.Item name="Activities">
                    <Button positive content="Create Activity" onClick={() => activityStore.openForm()}></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
};