import {
  Button,
  Container,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Paper,
  TextField,
  Typography
} from "@mui/material";
import {Visibility, VisibilityOff} from "@mui/icons-material";
import {useState} from "react";

function Auth() {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword(show => !show);

  const authEmployeeHandler = () => {

  }

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
        <Typography variant={"h5"} component={"h5"}>Авторизация</Typography>
        <FormControl sx={{m: 1, width: '25ch'}} variant="outlined">
          <TextField label="Логин" variant="outlined"/>
        </FormControl>
        <FormControl sx={{m: 1, width: '25ch'}} variant="outlined">
          <InputLabel htmlFor="outlined-adornment-password">Пароль</InputLabel>
          <OutlinedInput
            id="outlined-adornment-password"
            type={showPassword ? 'text' : 'password'}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff/> : <Visibility/>}
                </IconButton>
              </InputAdornment>
            }
            label="Пароль"
          />
        </FormControl>
        <Button variant="contained" onClick={authEmployeeHandler}>
          Авторизоваться
        </Button>
      </Paper>
    </Container>
  )
}

export {Auth}
