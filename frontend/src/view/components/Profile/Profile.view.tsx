import {useProfileState} from './Profile.state';
import {Card, Tag, Typography} from 'antd';

const {Text} = Typography;

export const ProfileView = ({user}: ReturnType<typeof useProfileState>) => {
    return (
        <Card
            title="Профиль пользователя"
        >
            {user ? (
                <>
                    {
                        user.roles && user.roles.map((role) => (
                            <Tag color="blue" style={{marginRight: 10}}>{role}</Tag>
                        ))
                    }
                    <Text>
                        {user.login}
                    </Text>
                </>
            ) : (
                <Text type="secondary">Данные не загружены</Text>
            )}
        </Card>
    );
};
