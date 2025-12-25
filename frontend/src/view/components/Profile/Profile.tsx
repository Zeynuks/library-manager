import { ProfileView } from './Profile.view';
import { useProfileState } from './Profile.state';

export const Profile = () => <ProfileView {...useProfileState()} />;