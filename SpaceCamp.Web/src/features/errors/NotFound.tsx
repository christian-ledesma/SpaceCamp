import { Link } from "react-router-dom";
import { Button, Header, Icon, Segment } from "semantic-ui-react";

export const NotFound = () => {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Oops! Not found!
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/activities'>Go back to Activities</Button>
            </Segment.Inline>
        </Segment>
    );
};