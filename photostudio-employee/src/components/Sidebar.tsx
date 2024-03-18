import {useEffect, useRef, useState} from "react";
import {Box, Button, List} from "@mui/material";
import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";

interface Testtttt {
    message: string
}

export default function Sidebar() {
    const key = useRef(false)
    const connection = useRef<HubConnection | null>(null)
    // const [clients, setClients] = useState([])
    const [state, setState] = useState<Testtttt>({message: "Ping"});
    useEffect(() => {
        if (key.current)
            return;
        key.current = true;
        connection.current = new HubConnectionBuilder().withUrl("http://localhost:5111/clients", {
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets
        }).build()
        connection.current?.on("Updated", data => {
            console.log(data)
        })
        connection.current?.on("Receive", data => {
            console.log(data)
        })
        connection.current?.on("Pong", data => {
            const newState: Testtttt = JSON.parse(data)
            console.log(newState)
            setState(newState)
        })
        connection.current?.start().then((res) => {
            setTimeout(() => {
                connection.current?.send("Ping", JSON.stringify(state))
            }, 1000)
            console.log(res);
        }).catch(err => {
            console.log(err);
        })
    })
    return (
        <Box className={"sticky top-0 left-0 w-52 flex items-center h-screen bg-black"}>
            <List className={"align-middle grid gap-5 w-screen"}>
                <Button variant={"text"}>Клиенты</Button>
                <Button variant={"text"}>{state.message}</Button>
            </List>
        </Box>
    )
}