import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { Grid } from "semantic-ui-react"
import { Loading } from "../../app/layout/Loading";
import { useStore } from "../../app/stores/store";
import { ProfileContent } from "./ProfileContent";
import { ProfileHeader } from "./ProfileHeader";


export const ProfilePage = observer(() => {
    const { username } = useParams<{username: string}>();
    const { profileStore } = useStore();
    const { loadingProfile, loadProfile, profile, setActiveTab } = profileStore;

    useEffect(() => {
        if (username)
            loadProfile(username);
        return () => {
            setActiveTab(0);
        }
    }, [loadProfile, username, setActiveTab]);

    if (loadingProfile)
        return <Loading />

    return (
        <Grid>
            <Grid.Column width={16}>
                {profile &&
                    <>
                        <ProfileHeader profile={profile} />
                        <ProfileContent profile={profile} />
                    </>
                }
            </Grid.Column>
        </Grid>
    );
});
