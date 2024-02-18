using Riok.Mapperly.Abstractions;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using Sprout.Exam.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sprout.Exam.Common.Mapping
{
    [Mapper]
    public partial class EmployeeMapping
    {
        [MapProperty(nameof(CreateEmployeeRequestDto.TypeId), nameof(Employee.EmployeeTypeId))]
        public partial Employee MapFromCreate(CreateEmployeeRequestDto dto);

        [MapProperty(nameof(CreateEmployeeRequestDto.TypeId), nameof(Employee.EmployeeTypeId))]
        [MapperIgnoreTarget(nameof(Employee.Id))]
        public partial void MapFromEdit(EditEmployeeRequestDto dto, Employee entity);

        [MapProperty(nameof(Employee.EmployeeTypeId), nameof(ReadEmployeeDto.TypeId))]
        public partial ReadEmployeeDto MapFromEntity(Employee entity);
        public partial List<ReadEmployeeDto> MapFromEntities(List<Employee> entity);


       
        private int EmployeeTypeToId(Enums.EmployeeType employeeType) => (int)employeeType;
        private Enums.EmployeeType TypeIdToEnum(int employeeTypeId) => (Enums.EmployeeType)employeeTypeId;
    }
}
