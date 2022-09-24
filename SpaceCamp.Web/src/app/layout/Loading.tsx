import { Dimmer, Loader } from "semantic-ui-react";


interface Props {
    inverted?: boolean;
    content?: string;
}


export const Loading = ({ inverted = true }, props: Props) => {
    return (
        <Dimmer active={true} inverted={inverted}>
            <Loader content={props.content} />
        </Dimmer>
    );
};