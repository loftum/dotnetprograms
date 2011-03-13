using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ZenTester.Lib.Domain;
using ZenTester.Lib.ExtensionMethods;

namespace ZenTester.Lib.Parsing
{
    public class ZenParser
    {
        public Story Parse(XElement element)
        {
            return new Story
                {
                    Id = element.GetLongValue("id"),
                    Size = element.GetIntValue("size"),
                    Color = element.GetStringValue("color"),
                    Status = element.GetStringValue("status"),
                    Text = element.GetStringValue("text"),
                    Details = element.GetStringValue("details"),
                    Project = ParseProject(element.GetChild("project")),
                    Creator = ParseUser(element.GetChild("creator")),
                    Owner = ParseUser(element.GetChild("owner")),
                    Phase = ParsePhase(element.GetChild("phase")),
                    Steps = ParseSteps(element.GetChild("steps")),
                    MileStones = ParseMilestones(element.GetChild("milestones")),
                    Comments = ParseComments(element.GetChild("comments"))
                };
        }

        public IEnumerable<Comment> ParseComments(XElement comments)
        {
            return from c in comments.Elements("comment")
                   select ParseComment(c);
        }

        public Comment ParseComment(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Comment
                {
                    Id = element.GetLongValue("id"),
                    CreateTime = element.GetDateValue("createTime"),
                    Text = element.GetStringValue("text"),
                    Author = ParseUser(element.GetChild("author"))
                };
        }

        public IEnumerable<MileStone> ParseMilestones(XElement milestones)
        {
            return from m in milestones.Elements("milestone")
                   select ParseMileStone(m);
        }

        public MileStone ParseMileStone(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new MileStone
                {
                    Id = element.GetLongValue("id"),
                    Duration = element.GetLongValue("duration"),
                    StartTime = element.GetDateValue("startTime"),
                    EndTime = element.GetDateValue("endTime"),
                    Phase = ParsePhase(element.GetChild("phase"))
                };
        }

        public IEnumerable<Step> ParseSteps(XElement steps)
        {
            return from s in steps.Elements("step")
                   select ParseStep(s);
        }

        public Step ParseStep(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Step
                {
                    Id = element.GetLongValue("id"),
                    Duration = element.GetLongValue("duration"),
                    Type = element.GetStringValue("type"),
                    StartTime = element.GetDateValue("startTime"),
                    EndTime = element.GetDateValue("endTime")
                };
        }

        public Phase ParsePhase(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Phase
                {
                    Id = element.GetLongValue("id"),
                    Name = element.GetStringValue("name")
                };
        }

        public User ParseUser(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new User
                {
                    Id = element.GetLongValue("id"),
                    Email = element.GetStringValue("email"),
                    Name= element.GetStringValue("name"),
                    Username = element.GetStringValue("userName")
                };
        }

        public Project ParseProject(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Project
                {
                    Id = element.GetLongValue("id"),
                    Name = element.GetStringValue("name")
                };
        }
    }
}