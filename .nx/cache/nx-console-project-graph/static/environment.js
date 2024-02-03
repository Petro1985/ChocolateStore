window.exclude = [];
  window.watch = false;
  window.environment = 'release';
  window.localMode = 'build';

  window.appConfig = {
    showDebugger: false,
    showExperimentalFeatures: false,
    workspaces: [
      {
        id: 'local',
        label: 'local',
        projectGraphUrl: 'project-graph.json',
        taskGraphUrl: 'task-graph.json',
        taskInputsUrl: 'task-inputs.json',
        sourceMapsUrl: 'source-maps.json'
      }
    ],
    defaultWorkspaceId: 'local',
  };
  window.projectGraphResponse = {"hash":"d6ac7e36f5048d3128ee2b30a7823ca4b9f3418d3907cf33dac2dec7cea9416c","projects":[],"dependencies":{},"fileMap":{},"layout":{"appsDir":"apps","libsDir":"libs"},"affected":[],"focus":null,"groupByFolder":false,"exclude":[]};
    window.taskGraphResponse = {"taskGraphs":{},"errors":{},"plans":{}};
    window.expandedTaskInputsResponse = {};window.sourceMapsResponse = {};