import React from 'react';
import {Container} from "@mui/material";
import Carousel from 'react-material-ui-carousel'
import img from '../assets/images/App-1.jpg'
import img2 from '../assets/images/App-2.jpg'
import { Paper, CardMedia } from '@mui/material'

function App() {
	const items = [img, img2]
	return (
		<Container sx={{width: '40%'}}>
			<Carousel animation={"fade"} indicators={false} >
				{
					items.map((item, i) => <CardMedia component={'img'} key={i} src={item} />)
				}
			</Carousel>
		</Container>
	);
}

export default App;
