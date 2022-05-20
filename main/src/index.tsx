import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import {createTheme, ThemeProvider} from "@mui/material";
import {deepPurple, purple} from "@mui/material/colors";
import App from "./routes/App";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Auth from "./routes/Auth";
import Header from "./components/Header";
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFns';
import {LocalizationProvider} from '@mui/x-date-pickers';
import Profile from "./routes/Profile";
import {Provider} from "react-redux";
import {store} from './redux/store';
import Services from "./routes/Services";
import './assets/css/index.css';

const dark = createTheme({
	palette: {
		mode: 'light',
		primary: {
			main: purple["500"],
			contrastText: '#ffffff'
		},
		secondary: {
			main: deepPurple["700"],
			contrastText: '#ffffff'
		},
	},
});
ReactDOM.render(
	<React.StrictMode>
		<Provider store={store}>
			<ThemeProvider theme={dark}>
				<LocalizationProvider dateAdapter={AdapterDateFns}>
					<BrowserRouter>
						<Header/>
						<Routes>
							<Route path={"/"} element={<App/>}/>
							<Route path={"profile"} element={<Profile/>}/>
							<Route path={"register"} element={<Auth/>}/>
							<Route path={"services"} element={<Services/>}/>
							<Route path={"complects"} element={<div/>}/>
							<Route path={"halls"} element={<div/>}/>
							<Route path={"about"} element={<div/>}/>
						</Routes>
					</BrowserRouter>
				</LocalizationProvider>
			</ThemeProvider>
		</Provider>
	</React.StrictMode>,
	document.getElementById('root')
);

reportWebVitals();