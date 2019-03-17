import React from 'react';
import { View, Text, Button, ListView, Image } from 'react-native';
import styles from "./Styles.js";
import { FileSystem } from 'expo';
import { NavigationActions, StackActions } from 'react-navigation';

var ds = new ListView.DataSource({ rowHasChanged: (row1, row2) => row1 !== row2 });
const ICON_RECORD_BUTTON = require('./assets/images/recordings_button.png');
const labels = {
  style: {
    color: '#4272b8',
	fontWeight: 'bold',
	fontSize: 20,
  },
};

class RecordingsScreen extends React.Component {

	render(){
		return(
			<View style={styles.mainContainer}>
				<View style={styles.headerContainer}>
					<Image source={ICON_RECORD_BUTTON}/>
					<Text style={labels.style}>Recordings</Text>
				</View>
				<ListView
					enableEmptySections
					//style={styles.dataContainer}
					//contentContainerStyle={styles.dataContainer}
					dataSource={this.state.dataSource}
					renderRow={(rowData) =>
						<View style={styles.buttonContainer}>
							<Button color='#396FB6'
								onPress={ () => { this.getInfo(rowData+"") }}
								title = {rowData+""}
							/>
						</View>
					}
				/>
			</View>
		)
	}

	constructor(props){
        super(props);
        this.state = {
			dataSource: ds.cloneWithRows([]),
			resetActionPlayer: null,
        };
	}
	
	async getInfo(rowData){
		var file = rowData.substr(0, rowData.indexOf('.'));//get filename without extention
		var script = "";
		//get script for that filename within lessons table and try to get it to original format (take out JSON format stuff)
		await fetch('http://reaching4english-001-site1.itempurl.com/Script/scriptQuery.php?filename=' + file)
		.then(res => res.json())
		.then(data => script = JSON.stringify(data)
								.replace(/\\r/g, '\r')
								.replace(/\\n/g, '\n')
								.replace(/\\t/g, '\t')
								.replace('{','')
								.replace('}','')
								.replace('[','')
								.replace(']','')
								.replace('"text":','')

		);

		await this.setState({
			resetActionPlayer: StackActions.reset({
				index: 0,
				actions: [
					NavigationActions.navigate({ routeName: 'Player', 
						params: {
							user: this.props.navigation.state.params.user,
							environment: this.props.navigation.state.params.environment,
							topic: this.props.navigation.state.params.topic,
							lid: this.props.navigation.state.params.lid,
							textSubs: script,
							path: FileSystem.documentDirectory + 'recordings/' + rowData,
							name: rowData,
							connected: this.props.navigation.state.params.connected,
							fromRecording: true, 
						}
					}),
				],
			}
			), 
		});
		this.props.navigation.dispatch(this.state.resetActionPlayer);
	}
	
	componentDidMount() {
		this.setState({ dataSource: ds.cloneWithRows(this.props.navigation.state.params.recordings)});
	}
}

export default RecordingsScreen;
