import React, {useState} from 'react';
import generateHash from '../utils/hashGenerator';
import {Button, Stack, TextField, Typography} from "@mui/material";
import {LoginModel} from '../models/Models';
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store";
import AuthService from "../services/AuthService";

interface State {
  login: string,
  password: string
}

function Login() {
  const [values, setValues] = useState<State>({
    login: '',
    password: ''
  });
  const dispatch = useDispatch<AppDispatch>();

  const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({...values, [prop]: event.target.value.trim()});
  };

  const onClick = async () => {
    const data: LoginModel = {
      login: values.login,
      password: await generateHash(values.password)
    };
    AuthService.login(data).then((res) => {
      dispatch(res)
    })
  };

  return (
    <Stack spacing={1}>
      <Typography variant={"h5"} component={"h5"}>Авторизация</Typography>
      <TextField value={values.login} onChange={handleChange('login')} type={"email"} variant={"standard"}
                 size={"small"} label={"Почта"}/>
      <TextField value={values.password} onChange={handleChange('password')} type={"password"}
                 variant={"standard"} size={"small"} label={"Пароль"}/>
      <Button type={"button"} variant={"contained"} onClick={onClick}>Авторизоваться</Button>
    </Stack>
  );
}

export default Login;
