import { UserListView } from './UserList.view.tsx';
import { useUserListState } from './UserList.state.ts';

export const UserList = () => {
    const state = useUserListState();
    return <UserListView {...state} />;
};
