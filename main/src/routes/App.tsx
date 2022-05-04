import React from 'react';
import {YMaps, Map} from 'react-yandex-maps';
import axios from "axios";

function App() {
	const API_KEY = 'c8950d58-d7e5-4bbe-996e-9981ae99ac9d';
	const onClick = (e: any) => {
		what(e.get('coords'));
	}
	const url = 'https://geocode-maps.yandex.ru/1.x/?apikey=' + API_KEY + '&format=json&geocode='
	const what = (e: Array<number>) => {
		axios.get(url + e[1] + ',' + e[0]).then((res) => {
			console.log(res.data.response.GeoObjectCollection.featureMember[0].GeoObject.metaDataProperty.GeocoderMetaData.Address.Components)
		})
	}

	return (
		<YMaps query={{apikey: API_KEY, load: "package.full"}}>
			<Map defaultState={{center: [58.604368690570226, 49.66600701212868], zoom: 17}} onClick={onClick}
					 style={{height: '400px'}}/>
		</YMaps>
	);
}

export default App;
