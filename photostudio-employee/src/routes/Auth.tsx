import {
  Button,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Stack,
  TextField
} from "@mui/material";
import {Visibility, VisibilityOff} from "@mui/icons-material";
import {useState} from "react";

function Auth() {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword(show => !show);

  const authEmployeeHandler = () => {

  }

  return (
    <Stack>
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
    </Stack>
  )
}

export {Auth}
