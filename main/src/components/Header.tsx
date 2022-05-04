import React, {useEffect} from 'react';
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
	Box,
} from "@mui/material";
import WbSunnyRoundedIcon from '@mui/icons-material/WbSunnyRounded';
import {getCookie, removeCookie, setCookie} from 'typescript-cookie'
import {useNavigate} from "react-router-dom";
import img from '../assets/images/avatar.png';
import axios from "axios";
import {Answer} from "../Types";

const settings = [{title: 'Профиль', click: 'profile'}, {title: 'Выход', click: 'logout'}];
const pages = ['Услуги', 'Комплекты', 'Залы', 'О нас'];

function Header() {
	const [token, setToken] = React.useState<string | undefined>(undefined);
	const navigate = useNavigate();
	const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
	const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

	useEffect(() => {
		setToken(getCookie('access_token'));
		if (token === undefined) {
			const refrToken = getCookie('refresh_token');
			if (refrToken === undefined) return;
			axios.get("http://localhost:8888/auth/reauth?token=" + refrToken).then((res) => {
				const data: Answer = res.data as Answer;
				if (data.status) {
					setCookie("access_token", data.answer.access_token, {expires: 1, path: ''});
					setCookie("refresh_token", data.answer.refresh_token, {path: ''});
					setToken(getCookie('access_token'));
				}
			})
		}
	}, [token])

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
				removeCookie('access_token', {path: ''});
				removeCookie('refresh_token', {path: ''});
				navigate('/');
				setAnchorElUser(null);
				break;
			case "profile":
				navigate('profile');
				break;
		}
	}
	return (
		<AppBar position="static">
			<Container maxWidth="xl">
				<Toolbar disableGutters>
					<Typography onClick={() => {
						navigate("/")
					}} variant="h6" noWrap component="div" sx={{mr: 2, cursor: 'pointer', display: {xs: 'none', md: 'flex'}}}>
						Sunrise
					</Typography>

					<Box sx={{flexGrow: 1, display: {xs: 'flex', md: 'none'}}}>
						<IconButton size="large" aria-label="account of current user" aria-controls="menu-appbar"
												aria-haspopup="true" onClick={handleOpenNavMenu} color="inherit">
							<WbSunnyRoundedIcon/>
						</IconButton>
						<Menu id="menu-appbar" anchorEl={anchorElNav} anchorOrigin={{vertical: 'bottom', horizontal: 'left',}}
									keepMounted transformOrigin={{vertical: 'top', horizontal: 'left',}} open={Boolean(anchorElNav)}
									onClose={handleCloseNavMenu} sx={{display: {xs: 'block', md: 'none'},}}>
							{pages.map((page) => (
								<MenuItem key={page} onClick={handleCloseNavMenu}>
									<Typography textAlign="center">{page}</Typography>
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
							<Button key={page} onClick={handleCloseNavMenu} sx={{my: 2, color: 'white', display: 'block'}}>
								{page}
							</Button>
						))}
					</Box>

					<Box sx={{flexGrow: 0}}>
						{token !== undefined &&
                <>
                    <Tooltip title="Откыть меню">
                        <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                            <Avatar alt="Danperad" src={img}/>
                        </IconButton>
                    </Tooltip>
                    <Menu sx={{mt: '45px'}} id="menu-appbar" anchorEl={anchorElUser}
                          anchorOrigin={{vertical: 'top', horizontal: 'right',}} keepMounted
                          transformOrigin={{vertical: 'top', horizontal: 'right',}} open={Boolean(anchorElUser)}
                          onClose={handleCloseUserMenu}>
											{settings.map((setting) => (
												<MenuItem key={setting.click} onClick={() => onClick(setting.click)}>
													<Typography textAlign="center">{setting.title}</Typography>
												</MenuItem>
											))}
                    </Menu>
                </>
						}
						{token === undefined &&
                <>
                    <Button name={"auth"} onClick={() => onClick("auth")}
                            sx={{my: 2, color: 'white', display: 'block'}}>
                        Авторизация
                    </Button>
                </>
						}
					</Box>
				</Toolbar>
			</Container>
		</AppBar>
	)
}

export default Header;
