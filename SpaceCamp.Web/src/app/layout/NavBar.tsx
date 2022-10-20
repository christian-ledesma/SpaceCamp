import { NavLink } from "react-router-dom";
import { Button, Container, Menu } from "semantic-ui-react";

export const NavBar = () => {

    return (
        <Menu inverted fixed="top">
            <Container>
                <Menu.Item as={NavLink} to="/" exact>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: "5px" }} />
                    SpaceCamp
                </Menu.Item>
                <Menu.Item as={NavLink} to="/activities" name="Activities">
                </Menu.Item>
                <Menu.Item as={NavLink} to="/errors" name="Errors">
                </Menu.Item>
                <Menu.Item>
                    <Button as={NavLink} to="/newActivity" positive content="Create Activity"></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
};
