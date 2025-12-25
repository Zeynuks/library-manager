import { HeaderView } from './Header.view';
import {useHeaderState} from './Header.state';

export const Header = () => <HeaderView {...useHeaderState()} />;
