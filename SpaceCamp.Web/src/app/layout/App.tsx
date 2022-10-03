import React from 'react';
import { Container } from 'semantic-ui-react';
import { NavBar } from './NavBar';
import { ActivityDashboard } from '../../features/activities/dashboard/ActivitiesDashboard';
import { observer } from 'mobx-react-lite';
import { Route, Routes } from 'react-router-dom';
import Home from '../../features/home/Home';
import { ActivityForm } from '../../features/activities/form/ActivityForm';
import { ActivityDetails } from '../../features/activities/details/ActivityDetails';

function App() {

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/activities" element={<ActivityDashboard />} />
          <Route path="/activities/:id" element={<ActivityDetails />} />
          <Route path="/activities/new" element={<ActivityForm />} />
          <Route path="activities/edit/:id" element={<ActivityForm />} />
        </Routes>
      </Container>
    </>
  );
}

export default observer(App);
