import { Link } from "react-router-dom";
import { Container } from "semantic-ui-react";

export const Home = () => {
    return (
        <Container style={{ marginTop: "7em" }}>
            <h1>Home Page!</h1>
            <Link to={"/activities"} >Activities</Link>
        </Container>
    );
};

export default Home;
