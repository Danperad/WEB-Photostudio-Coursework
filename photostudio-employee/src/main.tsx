import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import Sidebar from "./components/Sidebar.tsx";
import {Stack} from "@mui/material";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <Stack direction={"row"}>
          <Sidebar/>
          <App/>
      </Stack>
  </React.StrictMode>,
)
