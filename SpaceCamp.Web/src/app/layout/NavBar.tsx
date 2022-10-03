import { NavLink } from "react-router-dom";
import { Button, Container, Menu } from "semantic-ui-react";

export const NavBar = () => {

    return (
        <Menu inverted fixed="top">
            <Container>
                <Menu.Item as={NavLink} to="/" end>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: "5px" }} />
                    SpaceCamp
                </Menu.Item>
                <Menu.Item as={NavLink} to="/activities" name="Activities" end>
                </Menu.Item>
                <Menu.Item>
                    <Button as={NavLink} to="/activities/new" positive content="Create Activity"></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
};
