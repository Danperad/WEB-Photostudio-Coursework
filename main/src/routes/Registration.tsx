import React from 'react';
import {Button, Container, TextField, Typography} from "@mui/material";
import '../assets/css/header.css';
import axios from "axios";
import sha256 from "sha256";

type RegData = {
	login: string,
	lastname: string,
	firstname: string,
	phone: string,
	middlename: string,
	email: string,
	mainpassword: string,
	passwordcheck: string
}
type Answer = {
	login: string,
	password: string,
	lastname: string,
	firstname: string,
	phone: string,
	middlename: string,
	email: string
}
class Login extends React.Component<{}, RegData> {
	state: RegData = {
		login: '',
		lastname: '',
		firstname: '',
		phone: '',
		middlename: '',
		email: '',
		mainpassword: '',
		passwordcheck: ''
	};

	onClick = (event: any) => {
		if (this.state.mainpassword !== this.state.passwordcheck) return;
		const data : Answer = {
			login: this.state.login,
			password: sha256(this.state.mainpassword),
			lastname: this.state.lastname,
			firstname: this.state.firstname,
			phone: this.state.phone,
			middlename: this.state.middlename,
			email: this.state.email
		};
		axios.post("http://localhost:8888/auth/signon", data)
			.then((res) => {
				console.log(res.data);
			}).catch((err) => {
			alert(err)
		});
	}

	handleChange = (event: any) => {
		switch (event.target.name) {
			case 'login':
				this.setState({login: event.target.value.trim()});
				break;
			case 'email':
				this.setState({email: event.target.value.trim()});
				break;
			case 'lastName':
				this.setState({lastname: event.target.value.trim()});
				break;
			case 'firstName':
				this.setState({firstname: event.target.value.trim()});
				break;
			case 'middleName':
				this.setState({middlename: event.target.value.trim()});
				break;
			case 'phone':
				this.setState({phone: event.target.value.trim()});
				break;
			case 'password':
				this.setState({mainpassword: event.target.value.trim()});
				break;
			case 'passwordcheck':
				this.setState({passwordcheck: event.target.value.trim()});
				break;
		}
	};

	render() {
		return (
			<Container>
				<Typography variant={"h5"} component={"h5"} color={"white"}>Регистрация</Typography>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Логин</Typography>
				<TextField name={"login"} value={this.state.login} onChange={this.handleChange} type={"text"} color={"primary"}
									 variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Почта</Typography>
				<TextField name={"email"} value={this.state.email} onChange={this.handleChange} type={"text"} color={"primary"}
									 variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Фамилия</Typography>
				<TextField name={"lastName"} value={this.state.lastname} onChange={this.handleChange} type={"text"}
									 color={"primary"} variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Имя</Typography>
				<TextField name={"firstName"} value={this.state.firstname} onChange={this.handleChange} type={"text"}
									 color={"primary"} variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Отчество (если есть)</Typography>
				<TextField name={"middleName"} value={this.state.middlename} onChange={this.handleChange} type={"text"}
									 color={"primary"} variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Телефон</Typography>
				<TextField name={"phone"} value={this.state.phone} onChange={this.handleChange} type={"text"} color={"primary"}
									 variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Пароль</Typography>
				<TextField name={"password"} value={this.state.mainpassword} onChange={this.handleChange} type={"password"}
									 color={"primary"} variant={"outlined"}/>
				<Typography variant={"h6"} component={"h6"} color={"white"}>Повторите пароль</Typography>
				<TextField name={"passwordcheck"} value={this.state.passwordcheck} onChange={this.handleChange}
									 type={"password"} color={"primary"} variant={"outlined"}/>
				<Button type={"button"} variant={"outlined"} onClick={this.onClick}>Авторизоваться</Button>
			</Container>
		);
	}
}

export default Login;
