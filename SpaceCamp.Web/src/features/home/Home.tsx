import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";

export const Home = () => {
    return (
        <Segment inverted textAlign="center" vertical className="masthead">
            <Container text>
                <Header as="h1" inverted>
                    <Image size="massive" src="/assets/logo.png" alt="logo" style={{marginBottom: 12}} />
                </Header>
                <Button as={Link} to="/activities" size="huge" inverted>
                    Go to Activities
                </Button>
            </Container>
        </Segment>
    );
};

export default Home;
