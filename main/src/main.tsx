import React from 'react'
import ReactDOM from 'react-dom/client'
import {createTheme, ThemeProvider} from "@mui/material";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFnsV3';
import {LocalizationProvider} from '@mui/x-date-pickers';
import {Provider} from "react-redux";
import {store} from './redux/store';
import './assets/css/index.css';
import {SnackbarProvider} from "notistack";
import Services from "./routes/Services";
import Profile from "./routes/Profile";
import ShoppingCart from "./routes/ShoppingCart";
import Header from "./components/Header";
import NotifyCard from "./components/NotifyCard.tsx";
import Halls from "./routes/Halls";
import ServicePackages from "./routes/ServicePackages";
import Registration from "./routes/Registration.tsx";

const dark = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#8785A2',
      contrastText: '#ffffff'
    },
    secondary: {
      main: '#F6F6F6',
      contrastText: '#1e1e1e'
    },
  },
});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={dark}>
        <SnackbarProvider maxSnack={3}>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <BrowserRouter>
              <Header/>
              <Routes>
                <Route path={"/"} element={<Services/>}/>
                <Route path={"/halls"} element={<Halls/>}/>
                <Route path={"/packages"} element={<ServicePackages/>}/>
                <Route path={"/profile"} element={<Profile/>}/>
                <Route path={"/cart"} element={<ShoppingCart/>}/>
                <Route path={"/register"} element={<Registration/>}/>
                <Route path={"*"} element={<Navigate to={"/"}/>}/>
              </Routes>
              <NotifyCard/>
            </BrowserRouter>
          </LocalizationProvider>
        </SnackbarProvider>
      </ThemeProvider>
    </Provider>
  </React.StrictMode>,
)
