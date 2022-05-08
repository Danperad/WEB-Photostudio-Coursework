import React, {useEffect} from 'react';
import '../assets/css/header.css';
import {useNavigate} from "react-router-dom";
import Login from '../components/Login';
import Registration from "../components/Registration";
import {Stack} from "@mui/material";
import {useSelector} from "react-redux";
import {RootState} from "../redux/store";

function Auth() {
	const navigate = useNavigate();
	const user = useSelector((state: RootState) => state);

	useEffect(() => {
		if (user.auth) {
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
