import React from 'react';
import {AppBar, Box, Typography} from "@mui/material";

class Footer extends React.Component{
	render() {
		return (
			<footer>
				<Box sx={{display: 'flex', alignItems: 'center', textAlign: 'center' }}>
					<AppBar position={"fixed"}>
						<Typography variant={"h6"} component={"p"}>Hello</Typography>
					</AppBar>
				</Box>
			</footer>
		);
	}
}

export default Footer;
