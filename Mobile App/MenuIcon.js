import React from 'react';
import { View, Text, Image, Linking } from 'react-native';
import email from 'react-native-email';
import styles from "./Styles.js";
import {
  MenuProvider,
  Menu,
  MenuOptions,
  MenuOption,
  MenuTrigger,
} from 'react-native-popup-menu';

const about =   "Version: 1.0.0 \n"+
				"Owner: Stan Pichinevskiy \n"+
				"Developed by: \n"+
				"Jordan Lambert \n"+
				"Justin O'Neel \n"+
				"Daric Sage \n"+
				"Jared Regan \n\n"+

				"English listening assistant for Educative English Material. \n"+
				"For information on how to use the app please visit: http://reachingforenglish.com/support.html \n";
const labels = {
  triggerText: {
    color: '#4272b8',
	fontWeight: 'bold',
	fontSize: 20,
  },
};
export default class MenuIcon extends React.Component {	
	render(){
		return(
			<View>
				<Menu onSelect={value => {
					if(value == "about")
						alert(about);
					else if(value == "report")
						email('reachingforenglish@gmail.com', {subject: 'Report A Bug', body: 'Please report your bug here.'}).catch(console.error);
					else if(value == "survey"){
						try{ Linking.openURL('http://reachingforenglish.com/FileViewer.aspx'); }
						catch(e){ alert('Cannot open survey');}
					}
					else if(value == "help"){
						try{ Linking.openURL('http://reachingforenglish.com/support.html'); }
						catch(e){ alert('Cannot open support page'); }
					}
				}}>
					<MenuTrigger text='Menu     ' customStyles={labels} /> 
					<MenuOptions optionsContainerStyle={{backgroundColor:'#4272b8'}} customStyles={{optionsWrapper:{borderColor: 'white', borderWidth: 1},}}>
						<MenuOption value={"about"}>
							<Text style={{color: 'white', fontSize:20}}>About</Text>
						</MenuOption>
						<MenuOption value={"survey"}>
							<Text style={{color: 'white', fontSize:20}}>Survey/Printables</Text>
						</MenuOption>
						<MenuOption value={"help"}>
							<Text style={{color: 'white', fontSize:20}}>Help</Text>
						</MenuOption>
						<MenuOption value={"report"}>
							<Text style={{color: 'white', fontSize:20}}>Report Error</Text>
						</MenuOption>
					</MenuOptions>
				</Menu>
			</View>
		);
	}
	constructor(props){
		super(props);
	}
}