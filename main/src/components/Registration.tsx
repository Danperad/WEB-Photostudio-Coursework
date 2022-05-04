import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import {setCookie} from "typescript-cookie";
import sha256 from "sha256";
import axios from "axios";
import {Answer} from "../Types";
import {Button, Stack, TextField, Typography} from "@mui/material";

type Request = {
	login: string,
	password: string,
	lastname: string,
	firstname: string,
	phone: string,
	middlename: string,
	email: string
}

interface State {
	login: string,
	lastname: string,
	firstname: string,
	phone: string,
	middlename: string,
	email: string,
	mainpassword: string,
	passwordcheck: string
}

function Registration() {
	const [values, setValues] = useState<State>({
		login: '',
		lastname: '',
		firstname: '',
		phone: '',
		middlename: '',
		email: '',
		mainpassword: '',
		passwordcheck: ''
	})
	const navigate = useNavigate();

	const onClick = (event: any) => {
		if (values.mainpassword !== values.passwordcheck) return;
		const data: Request = {
			login: values.login,
			password: sha256(values.mainpassword),
			lastname: values.lastname,
			firstname: values.firstname,
			phone: values.phone,
			middlename: values.middlename,
			email: values.email
		};
		axios.post("http://localhost:8888/auth/signon", data)
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

	const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
		setValues({...values, [prop]: event.target.value.trim()});
	};

	return (
		<Stack spacing={1}>
			<Typography variant={"h5"} component={"h5"} color={"white"}>Регистрация</Typography>
			<Stack direction={"row"} spacing={1}>
				<Stack spacing={1}>
					<TextField label={"Фамилия"} value={values.lastname} onChange={handleChange('lastname')} type={"text"}
										 color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Отчество (если есть)"} value={values.middlename} onChange={handleChange('middlename')}
										 type={"text"} color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Почта"} value={values.email} onChange={handleChange('email')} type={"text"}
										 color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Пароль"} value={values.mainpassword} onChange={handleChange('mainpassword')}
										 type={"password"} color={"primary"} variant={"outlined"} size={"small"}/>
				</Stack>
				<Stack spacing={1}>
					<TextField label={"Имя"} value={values.firstname} onChange={handleChange('firstname')} type={"text"}
										 color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Телефон"} value={values.phone} onChange={handleChange('phone')} type={"text"}
										 color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Логин"} value={values.login} onChange={handleChange('login')} type={"text"}
										 color={"primary"} variant={"outlined"} size={"small"}/>
					<TextField label={"Повторите пароль"} value={values.passwordcheck} onChange={handleChange('passwordcheck')}
										 type={"password"} color={"primary"} variant={"outlined"} size={"small"}/>
				</Stack>
			</Stack>
			<Button type={"button"} variant={"outlined"} onClick={onClick}>Регистрация</Button>
		</Stack>
	);
}

export default Registration;