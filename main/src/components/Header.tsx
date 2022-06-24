import React from 'react';
import {
    AppBar,
    Button,
    Container,
    Toolbar,
    Typography,
    Tooltip,
    IconButton,
    Avatar,
    Menu,
    MenuItem,
    Box, Badge
} from "@mui/material";
import WbSunnyRoundedIcon from '@mui/icons-material/WbSunnyRounded';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import {useNavigate} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store";
import AuthService from "../services/AuthService"
import AuthModal from "./AuthModal";
import ClientService from "../services/ClientService";
import {CloseModal, OpenModal} from "../redux/actions/authModalActions";


const settings = [{title: 'Профиль', click: 'profile'}, {title: 'Выход', click: 'logout'}];
const pages = [{title: 'Услуги', click: '/'}, {title: 'Комплекты', click: 'packages'}, {
    title: 'Залы',
    click: 'halls'
}];

function Header() {
    const navigate = useNavigate();
    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
    const [key, setKey] = React.useState<boolean>(false);
    const user = useSelector((state: RootState) => state);
    const dispatch = useDispatch<AppDispatch>();

    React.useEffect(() => {
        if (key) return;
        setKey(true);
        ClientService.loadClient().then((res) => {
            dispatch(res);
        })
    }, [key, dispatch])

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const onClick = (key: string) => {
        switch (key) {
            case "auth":
                navigate('auth');
                break;
            case "logout":
                dispatch(AuthService.logout())
                navigate('/');
                setAnchorElUser(null);
                break;
            case "profile":
                navigate('profile');
                setAnchorElUser(null);
                break;
            case "/":
                navigate('/');
                setAnchorElUser(null);
                break;
            case "packages":
                navigate('packages');
                setAnchorElUser(null);
                break;
            case "halls":
                navigate('halls');
                setAnchorElUser(null);
                break;
            case "cart":
                navigate('cart');
                setAnchorElUser(null);
                break;
            default:
                setAnchorElUser(null);
                break;
        }
    }

    const cartLength = () => {
        return  user.cart.servicePackage !== null ? user.cart.serviceModels.length + 1 : user.cart.serviceModels.length
    }

    return (
        <>
            <AppBar>
                <Container maxWidth={false}>
                    <Toolbar disableGutters>
                        <Typography onClick={() => {
                            navigate("/")
                        }} variant="h6" noWrap component="div"
                                    sx={{mr: 2, cursor: 'pointer', display: {xs: 'none', md: 'flex'}}}>
                            Sunrise
                        </Typography>

                        <Box sx={{flexGrow: 1, display: {xs: 'flex', md: 'none'}}}>
                            <IconButton size="large" aria-label="account of current user" aria-controls="menu-appbar"
                                        aria-haspopup="true" onClick={handleOpenNavMenu} color="inherit">
                                <WbSunnyRoundedIcon/>
                            </IconButton>
                            <Menu id="menu-appbar" anchorEl={anchorElNav}
                                  anchorOrigin={{vertical: 'bottom', horizontal: 'left',}}
                                  keepMounted transformOrigin={{vertical: 'top', horizontal: 'left',}}
                                  open={Boolean(anchorElNav)}
                                  onClose={handleCloseNavMenu} sx={{display: {xs: 'block', md: 'none'},}}>
                                {pages.map((page) => (
                                    <MenuItem key={page.click} onClick={() => onClick(page.click)}>
                                        <Typography textAlign="center">{page.title}</Typography>
                                    </MenuItem>
                                ))}
                            </Menu>
                        </Box>
                        <Typography onClick={() => {
                            navigate("/")
                        }} variant="h6" noWrap component="div"
                                    sx={{flexGrow: 1, cursor: 'pointer', display: {xs: 'flex', md: 'none'}}}>
                            Sunrise
                        </Typography>
                        <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}>
                            {pages.map((page) => (
                                <Button key={page.click} onClick={() => onClick(page.click)}
                                        sx={{my: 2, display: 'block'}}
                                        color={"inherit"}>
                                    {page.title}
                                </Button>
                            ))}
                        </Box>
                        <Box sx={{flexGrow: 0, display: {md: 'flex'}}}>
                            <IconButton onClick={() => onClick("cart")} sx={{mr: 1}}>
                                <Badge badgeContent={cartLength()} color="secondary">
                                    <ShoppingCartIcon />
                                </Badge>
                            </IconButton>
                            {user.client.isAuth && user.client.client !== null ? (
                                <>
                                    <Tooltip title="Откыть меню">
                                        <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                                            <Avatar alt={user.client.client!.login.toUpperCase()}
                                                    src={user.client.client!.avatar}/>
                                        </IconButton>
                                    </Tooltip>
                                    <Menu sx={{mt: '45px'}} id="menu-appbar" anchorEl={anchorElUser}
                                          anchorOrigin={{vertical: 'top', horizontal: 'right',}} keepMounted
                                          transformOrigin={{vertical: 'top', horizontal: 'right',}}
                                          open={Boolean(anchorElUser)}
                                          onClose={handleCloseUserMenu}>
                                        {settings.map((setting) => (
                                            <MenuItem key={setting.click} onClick={() => onClick(setting.click)}>
                                                <Typography textAlign="center">{setting.title}</Typography>
                                            </MenuItem>
                                        ))}
                                    </Menu>
                                </>
                            ) : (
                                <>
                                    <Button name={"auth"} onClick={() => dispatch(OpenModal(true))} sx={{my: 2, display: 'block'}}
                                            color={"inherit"}>
                                        Авторизация
                                    </Button>
                                    <AuthModal open={user.authModal} handlerClose={() => dispatch(CloseModal(false))}/>
                                </>
                            )
                            }
                        </Box>
                    </Toolbar>
                </Container>
            </AppBar>
            <Toolbar/>
        </>
    )
}

export default Header;
