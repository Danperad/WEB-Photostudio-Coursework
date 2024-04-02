import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import generateHash from '../utils/hashGenerator.ts';
import {RegistrationModel} from '../models/Models.ts'
import {Button, FormControl, Stack, TextField, Typography} from "@mui/material";
import AuthService from "../services/AuthService.ts";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store.ts";
import {clientActions} from "../redux/slices/clientSlice.ts";

interface State {
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
    lastname: '',
    firstname: '',
    phone: '',
    middlename: '',
    email: '',
    mainpassword: '',
    passwordcheck: ''
  })
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();

  const onClick = async (_: any) => {
    if (values.mainpassword !== values.passwordcheck) return;
    const data: RegistrationModel = {
      password: await generateHash(values.mainpassword),
      lastName: values.lastname,
      firstName: values.firstname,
      phone: values.phone,
      middleName: values.middlename,
      eMail: values.email
    };
    AuthService.register(data).then((res) => {
      dispatch(res)
      if (res.type === clientActions.registerSuccess.type) {
        navigate("/")
      }
    });
  };

  const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({...values, [prop]: event.target.value.trim()});
  };

  const handlePhoneChange = (e: any) => {
    setValues({...values, phone: e.target.value.replace(/\D/g, '')})
  };

  return (
    <FormControl>
      <Stack spacing={1}>
        <Typography variant={"h5"} component={"h5"} color={"white"}>Регистрация</Typography>
        <Stack direction={"row"} spacing={1}>
          <Stack spacing={1}>
            <TextField label={"Фамилия"} value={values.lastname} onChange={handleChange('lastname')} type={"text"}
                       color={"primary"} variant={"outlined"} size={"small"} required/>
            <TextField label={"Отчество (если есть)"} value={values.middlename} onChange={handleChange('middlename')}
                       type={"text"} color={"primary"} variant={"outlined"} size={"small"}/>
            <TextField label={"Почта"} value={values.email} onChange={handleChange('email')} type={"email"}
                       color={"primary"} variant={"outlined"} size={"small"} required/>
            <TextField label={"Пароль"} value={values.mainpassword} onChange={handleChange('mainpassword')}
                       type={"password"} color={"primary"} variant={"outlined"} size={"small"} required/>
          </Stack>
          <Stack spacing={1}>
            <TextField label={"Имя"} value={values.firstname} onChange={handleChange('firstname')} type={"text"}
                       color={"primary"} variant={"outlined"} size={"small"} required/>
            <TextField type={"tel"} label={"Телефон"} value={values.phone} onChange={handlePhoneChange}
                       color={"primary"} variant={"outlined"} size={"small"} required/>
            <TextField label={"Повторите пароль"} value={values.passwordcheck} onChange={handleChange('passwordcheck')}
                       type={"password"} color={"primary"} variant={"outlined"} size={"small"} required/>
          </Stack>
        </Stack>
        <Button type={"button"} variant={"contained"} color={"secondary"} onClick={onClick}
                disableElevation>Регистрация</Button>
      </Stack>
    </FormControl>
  );
}

export default Registration;
