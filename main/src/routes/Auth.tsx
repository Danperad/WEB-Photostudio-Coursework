import React, {useEffect} from 'react';
import '../assets/css/header.css';
import {getCookie} from "typescript-cookie";
import {useNavigate} from "react-router-dom";
import Login from '../components/Login';
import Registration from "../components/Registration";
import {Stack} from "@mui/material";

function Auth() {
	const navigate = useNavigate();

	useEffect(() => {
		if (getCookie('access_token') !== undefined) {
			navigate("/");
		}
	});
	return (
		<Stack direction={"row"} alignItems={"center"} justifyContent={"center"} spacing={15}>
			<Login/>
			<Registration/>
		</Stack>
	)
}

export default Auth;
