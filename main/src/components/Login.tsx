import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import {setCookie} from "typescript-cookie";
import sha256 from "sha256";
import axios from "axios";
import { Answer } from "../Types";
import {Button, Stack, TextField, Typography} from "@mui/material";

interface State {
	login: string,
	password: string
}

function Login() {
	const [values, setValues] = useState<State>({
		login: '',
		password: ''
	});
	const navigate = useNavigate();

	const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
		setValues({ ...values, [prop]: event.target.value.trim()});
	};

	const onClick = () => {
		const data = {
			login: values.login,
			password: sha256(values.password)
		};
		axios.post("http://localhost:8888/auth/signin", data)
			.then((res) => {
				const data: Answer = res.data as Answer;
				if (data.status) {
					setCookie("access_token", data.answer.access_token, {expires: 1, path: ''});
					setCookie("refresh_token", data.answer.refresh_token, {path: ''});
					navigate("/");
				} else {
					alert("Не успешно");
				}
			}).catch((err) => {
			alert(err)
		});
	};

	return (
		<Stack spacing={1}>
			<Typography variant={"h5"} component={"h5"} color={"white"} >Авторизация</Typography>
			<TextField value={values.login} onChange={handleChange('login')} type={"text"} color={"primary"}
								 variant={"outlined"} size={"small"} label={"Логин"} />
			<TextField value={values.password} onChange={handleChange('password')} type={"password"} color={"primary"}
								 variant={"outlined"} size={"small"} label={"Пароль"}/>
			<Button type={"button"} variant={"outlined"} onClick={onClick}>Авторизоваться</Button>
		</Stack>
	);
}

export default Login;