import {Box, Button, List} from "@mui/material";

export default function Sidebar() {
    return (
        <Box className={"sticky top-0 left-0 w-52 flex items-center h-screen bg-black"}>
            <List className={"align-middle grid gap-5 w-screen"}>
                <Button variant={"text"}>Клиенты</Button>
                <Button variant={"text"}>Клиенты</Button>
            </List>
        </Box>
    )
}