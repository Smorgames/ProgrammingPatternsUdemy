using System;
using System.Collections.Generic;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Mediator();
            
            var p1 = new Participant(m);
            var p2 = new Participant(m);
            var p3 = new Participant(m);

            Console.WriteLine($"p1 = {p1.Value}; p2 = {p2.Value}; p3 = {p3.Value}");
            
            p1.Say(3);
            
            Console.WriteLine($"p1 = {p1.Value}; p2 = {p2.Value}; p3 = {p3.Value}");

            p2.Say(2);
            
            Console.WriteLine($"p1 = {p1.Value}; p2 = {p2.Value}; p3 = {p3.Value}");
        }
    }
    
    public class Participant
    {
        public int Value { get; set; }
        private Mediator _mediator;

        public Participant(Mediator mediator)
        {
            _mediator = mediator;
            _mediator.Register(this);
        }

        public void Say(int n)
        {
            _mediator.Say(n, this);
        }
    }

    public class Mediator
    {
        private List<Participant> _participants;

        public Mediator()
        {
            _participants = new List<Participant>();
        }

        public void Register(Participant participant)
        {
            _participants.Add(participant);
        }

        public void Say(int n, Participant participant)
        {
            foreach (var p in _participants)
                if (p != participant) 
                    p.Value = n;
        }
    }
}
