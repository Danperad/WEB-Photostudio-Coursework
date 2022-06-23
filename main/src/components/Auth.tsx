import React, {useEffect} from 'react';
import {useNavigate} from "react-router-dom";
import Registration from "../components/Registration";
import {Stack} from "@mui/material";
import {useSelector} from "react-redux";
import {RootState} from "../redux/store";

function Auth() {
	const navigate = useNavigate();
	const rootState = useSelector((state: RootState) => state);

	useEffect(() => {
		if (rootState.client.isAuth) {
			navigate("/");
		}
	});
	return (
		<Stack direction={"row"} alignItems={"center"} justifyContent={"center"} spacing={15}>
			<Registration/>
		</Stack>
	)
}

export default Auth;
