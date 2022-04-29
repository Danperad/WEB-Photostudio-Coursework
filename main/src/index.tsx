import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import {createTheme, ThemeProvider} from "@mui/material";
import {blue, grey} from "@mui/material/colors";
import App from "./App";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./routes/Login";
import "./assets/css/main.css"
import Header from "./components/Header";
import Registration from "./routes/Registration";


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
			<BrowserRouter>
				<Header/>
				<Routes>
					<Route path={"/"} element={<App/>}/>
					<Route path={"login"} element={<Login/>}/>
					<Route path={"registration"} element={<Registration/>}/>
				</Routes>
			</BrowserRouter>
		</ThemeProvider>
	</React.StrictMode>,
	document.getElementById('root')
);

reportWebVitals();
