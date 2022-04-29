import React from 'react';
import {Button, Container, TextField, Typography} from "@mui/material";
import '../assets/css/header.css';
import axios from "axios";

type AuthData = {
	login: string,
	password: string
}

class Login extends React.Component<{}, AuthData> {
	state: AuthData = {
		login: '',
		password: ''
	};

	onClick = (event: any) => {
		axios.post("http://localhost:8888/auth/signin", this.state, {headers: {"Access-Control-Allow-Origin": "*"}})
			.then((res) =>{
				console.log(res.data);
			}).catch((err) => {
				alert(err)
		});
	}

	handleChange = (event: any) => {
		if (event.target.type === 'text')
			this.setState({login: event.target.value, password: this.state.password});
		else
			this.setState({login: this.state.login, password: event.target.value});
	};

	render() {
		return (
			<Container>
				<Typography variant={"h5"} component={"h5"} color={"white"}>Авторизация</Typography>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Логин</Typography>
				<TextField value={this.state.login} onChange={this.handleChange} type={"text"} color={"primary"}
									 variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Пароль</Typography>
				<TextField value={this.state.password} onChange={this.handleChange} type={"password"} color={"primary"}
									 variant={"outlined"}/>
				<Button type={"button"} variant={"outlined"} onClick={this.onClick}>Авторизоваться</Button>
			</Container>
		);
	}
}

export default Login;
