import React, { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';

function App() {
  const [activities, setActivivies] = useState([]);
  useEffect(() => {
    axios.get("https://localhost:5001/api/v1/activities").then((response) => {
      setActivivies(response.data);
    });
  }, []);
  return (
    <div>
      <Header as='h2' icon="users" content="SpaceCamp" />
      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>
            {activity.name}
          </List.Item>
        ))}
      </List>
    </div>
  );
}

export default App;
