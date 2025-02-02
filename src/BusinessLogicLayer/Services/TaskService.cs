﻿using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class TaskService : ITaskService
    {
        IRepository<Task> TaskRepository { get; set; }
        public TaskService(IRepository<Task> taskRepository)
        {
            TaskRepository = taskRepository;
        }

        public void CreateTask(TaskDTO taskDTO)
        {
            Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
            TaskRepository.Create(task);
        }
        public IEnumerable<TaskDTO> GetTasks()
        {
            List<TaskDTO> taskDTOs = new List<TaskDTO>();
            foreach (Task task in TaskRepository.GetAll())
            {
                TaskDTO taskDTO = Mapper.Convert<Task, TaskDTO>(task);
                taskDTO.Project = Mapper.Convert<Project, ProjectDTO>(task.Project);
                taskDTO.Status = Mapper.Convert<Status, StatusDTO>(task.Status);
                taskDTO.Employee = Mapper.Convert<Employee, EmployeeDTO>(task.Employee);
                taskDTOs.Add(taskDTO);
            }

            return taskDTOs;
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
            TaskRepository.Update(task);
        }
        public TaskDTO GetTask(int? id)
        {
            var task = TaskRepository.GetById(id.Value);
            if (task != null)
            {
                TaskDTO taskDTO = Mapper.Convert<Task, TaskDTO>(task);
                taskDTO.Project = Mapper.Convert<Project, ProjectDTO>(task.Project);
                taskDTO.Status = Mapper.Convert<Status, StatusDTO>(task.Status);
                taskDTO.Employee = Mapper.Convert<Employee, EmployeeDTO>(task.Employee);
                return taskDTO;
            }
            return null;
        }

        public void DeleteTask(int id)
        {
            TaskRepository.Delete(id);
        }
    }
}
