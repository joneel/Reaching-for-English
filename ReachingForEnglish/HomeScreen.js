import React from 'react';
import styles from "./Styles.js";
import { SQLite, Permissions } from 'expo';
import { View, Text, Button, ListView, NetInfo, Platform, Image, TouchableHighlight } from 'react-native';
import { NavigationActions, StackActions } from 'react-navigation';

const ICON_LOGO_BUTTON = require('./assets/images/logo3.png');

class HomeScreen extends React.Component {
	
	render(){
		return(		
			<View style={styles.homeContainer}>
				<TouchableHighlight
					onPress={this.handlePermissions}
					underlayColor="#fff">
					<Image source={ICON_LOGO_BUTTON}/>
				</TouchableHighlight>
			</View>
		);
	}

	constructor(props){
		super(props);
		this.state = {
			haveRecordingPermissions: false,
		};
		this.handlePermissions = this.handlePermissions.bind(this);
	}

	componentDidMount() {
		this.askForPermissions();
	}

	handlePermissions(){
		if(this.state.haveRecordingPermissions){
			this.props.navigation.navigate('User');
		}
		else{
			this.askForPermissions();
		}
	}
		
	askForPermissions = async () => {
		const response = await Permissions.askAsync(Permissions.AUDIO_RECORDING);
		this.setState({
			haveRecordingPermissions: response.status === 'granted',
		});
	};

}
export default HomeScreen;