import { LoginFormView } from './LoginForm.view.tsx';
import { useLoginFormState } from './LoginForm.state.ts';

export type LoginFormProps = {
    disabled?: boolean;
};

export const LoginForm = (props: LoginFormProps) => (
    <LoginFormView {...useLoginFormState(props)} />
);
