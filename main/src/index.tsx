import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import {createTheme, ThemeProvider} from "@mui/material";
import {blue, grey} from "@mui/material/colors";
import App from "./routes/App";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Auth from "./routes/Auth";
import "./assets/css/main.css"
import Header from "./components/Header";
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers';
import Profile from "./routes/Profile";

const dark = createTheme({
	palette: {
		mode: 'dark',
		divider: blue[700],
		background: {
			default: blue[900],
			paper: blue[900],
		},
		text: {
			primary: '#fff',
			secondary: grey[500],
		},
	},
});

ReactDOM.render(
	<React.StrictMode>
		<ThemeProvider theme={dark}>
			<LocalizationProvider dateAdapter={AdapterDateFns}>
			<BrowserRouter>
					<Header/>
					<Routes>
						<Route path={"/"} element={<App/>}/>
						<Route path={"profile"} element={<Profile/>}/>
						<Route path={"auth"} element={<Auth/>}/>
					</Routes>
				</BrowserRouter>
			</LocalizationProvider>
		</ThemeProvider>
	</React.StrictMode>,
	document.getElementById('root')
);

reportWebVitals();