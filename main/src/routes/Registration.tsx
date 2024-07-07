import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import generateHash from '../utils/hashGenerator.ts';
import {RegistrationModel} from '../models/Models.ts'
import {Button, Container, FormControl, Grid, Paper, TextField, Typography} from "@mui/material";
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

  const onClick = async () => {
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

  const handlePhoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValues({...values, phone: e.target.value.replace(/\D/g, '')})
  };

  return (
    <Container sx={{height: "90vh", display: "flex", alignItems: "center", justifyContent: "center"}}>
      <Paper elevation={2} sx={{
        py: 2,
        width: "30%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center"
      }}>
        <Typography variant={"h5"} component={"h5"}>Регистрация</Typography>
        <FormControl sx={{
          py: 2,
          width: "60%",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center"
        }}>
          <Grid container spacing={1} columns={1}>
            <Grid item xs={1}>
              <TextField label={"Фамилия"} value={values.lastname} onChange={handleChange('lastname')}
                         type={"text"} color={"primary"} variant={"outlined"} size={"small"}
                         required/>
            </Grid>
            <Grid item xs={1}>
              <TextField label={"Имя"} value={values.firstname} onChange={handleChange('firstname')}
                         type={"text"} color={"primary"} variant={"outlined"} size={"small"}
                         required/>
            </Grid>
            <Grid item xs={1}>
              <TextField label={"Отчество (если есть)"} value={values.middlename}
                         onChange={handleChange('middlename')} type={"text"} color={"primary"}
                         variant={"outlined"} size={"small"}/>
            </Grid>
            <Grid item xs={1}>
              <TextField label={"Почта"} value={values.email} onChange={handleChange('email')}
                         type={"email"}
                         color={"primary"} variant={"outlined"} size={"small"} required/>
            </Grid>
            <Grid item xs={1}>
              <TextField type={"tel"} label={"Телефон"} value={values.phone}
                         onChange={handlePhoneChange}
                         color={"primary"} variant={"outlined"} size={"small"} required/>
            </Grid>
            <Grid item xs={1}>
              <TextField label={"Пароль"} value={values.mainpassword}
                         onChange={handleChange('mainpassword')}
                         type={"password"} color={"primary"} variant={"outlined"} size={"small"}
                         required/>
            </Grid>
            <Grid item xs={1}>
              <TextField label={"Повторите пароль"} value={values.passwordcheck}
                         onChange={handleChange('passwordcheck')} type={"password"} color={"primary"}
                         variant={"outlined"} size={"small"} required/>
            </Grid>
          </Grid>
          <Button sx={{mt: 2}} type={"button"} variant={"contained"} color={"secondary"} onClick={onClick}
                  disableElevation>Регистрация</Button>
        </FormControl>
      </Paper>
    </Container>
  )

}

export default Registration;
