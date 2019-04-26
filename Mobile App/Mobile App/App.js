/*
*
* Use a StackNavigator for all the screens and wrap the 
* stack navigator within a MenuProvider from react-native-popup-menu
* so the menu will be on each screen. In ComponentWillMount
* we create the directory for saved recordings.
*
*/


import React from 'react';
import HomeScreen from "./HomeScreen.js";
import UserScreen from "./UserScreen.js";
import EnvironmentScreen from "./EnvironmentScreen.js";
import TopicScreen from "./TopicScreen.js";
import LessonScreen from "./LessonScreen.js";
import PlayerScreen from "./PlayerScreen.js";
import RecordingsScreen from "./RecordingsScreen.js";
import MenuIcon from "./MenuIcon.js";
import { FileSystem } from 'expo';
import { createStackNavigator, createAppContainer } from 'react-navigation';
import { MenuProvider } from 'react-native-popup-menu';
	
const RootNavigator = createStackNavigator({
	Home: {
		screen:HomeScreen,
		navigationOptions: {
			header: null,
		},
	},
	User: {
		screen: UserScreen,
		navigationOptions: {
			//headerTitle: 'User',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
		},
	},
	Environment: {
		screen: EnvironmentScreen,
		navigationOptions: {
			//headerTitle: 'Environment',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
		},
	},
	Topic: {
		screen: TopicScreen,
		navigationOptions: {
			//headerTitle: 'Topic',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
		},
	},
	Lesson: {
		screen: LessonScreen,
		navigationOptions: {
			//headerTitle: 'Lesson',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
		},
	},
	Player: {
    	screen: PlayerScreen,
    	navigationOptions: {
    		//headerTitle: 'Player',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
    	},
    },
	Recordings: {
    	screen: RecordingsScreen,
    	navigationOptions: {
    		//headerTitle: 'Recordings',
			headerTintColor: '#4272b8',
			headerRight: <MenuIcon />
    	},
    },
});


const App = createAppContainer(RootNavigator);
export default App