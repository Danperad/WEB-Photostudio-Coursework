import React, {useEffect} from 'react';
import {getCookie, setCookie} from "typescript-cookie";
import axios from "axios";
import {Answer, Client} from "../Types";
import {useNavigate} from "react-router-dom";
import {Stack, Typography} from "@mui/material";

function Profile() {
	const [token, setToken] = React.useState<string | undefined>(getCookie('access_token'));
	const navigate = useNavigate();
	const [client, setClient] = React.useState<Client>({
		id: 0,
		lastName: '',
		firstName: '',
		middleName: null,
		email: '',
		phone: '',
		login: '',
		company: null
	});
	useEffect(() => {
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
		if (token === undefined) {
			navigate("/auth")
			return;
		}
		if (client.id !== 0) return;
		axios.get("http://localhost:8888/client/get", {headers: {"Access-Token": token}})
			.then((res) => {
				const data: Answer = res.data as Answer;
				if (data.status) {
					setClient(data.answer);
				}
			})
	}, [token, navigate, client])
	return (
		<Stack>
			<Typography>{client.lastName}</Typography>
		</Stack>
	);
}

export default Profile;