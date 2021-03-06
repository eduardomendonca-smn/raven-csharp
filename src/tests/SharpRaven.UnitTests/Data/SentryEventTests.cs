﻿#region License

// Copyright (c) 2014 The Sentry Team and individual contributors.
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted
// provided that the following conditions are met:
// 
//     1. Redistributions of source code must retain the above copyright notice, this list of
//        conditions and the following disclaimer.
// 
//     2. Redistributions in binary form must reproduce the above copyright notice, this list of
//        conditions and the following disclaimer in the documentation and/or other materials
//        provided with the distribution.
// 
//     3. Neither the name of the Sentry nor the names of its contributors may be used to
//        endorse or promote products derived from this software without specific prior written
//        permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;

using NUnit.Framework;

using SharpRaven.Data;

namespace SharpRaven.UnitTests.Data
{
    [TestFixture]
    public class SentryEventTests
    {
        [Test]
        public void ShouldWhenConstructorTagsNotNull()
        {
            Assert.That(new SentryEvent(new Exception("foo")).Tags, Is.Not.Null);
        }

        [Test]
        public void ShouldNotNullDictionaryWhenAssignNullForTags()
        {
            var sentryEvent = new SentryEvent(new Exception("foo")) { Tags = null};

            Assert.That(sentryEvent.Tags, Is.Not.Null);
        }

        [Test]
        public void ShouldTheSameDictionaryFromConstructor()
        {
            var sentryEvent = new SentryEvent(new Exception("foo"));
            var beforeDictionary = sentryEvent.Tags;

            Assert.That(beforeDictionary, Is.SameAs(sentryEvent.Tags));
        }

        [Test]
        public void ShouldStartNewDictionaryWhenAsignNullForTags()
        {
            var sentryEvent = new SentryEvent(new Exception("foo"));
            var beforeDictionary = sentryEvent.Tags;

            sentryEvent.Tags = null;

            Assert.That(beforeDictionary, Is.Not.SameAs(sentryEvent.Tags));
        }

        [Test]
        public void ShouldHaveNonEmptyContextsFromException()
        {
            var sentryEvent = new SentryEvent(new Exception("foo"));

            NonEmptyContext(sentryEvent);
        }

        [Test]
        public void ShouldHaveNonEmptyContextsFromMessage()
        {
            var sentryEvent = new SentryEvent(new SentryMessage("foo"));

            NonEmptyContext(sentryEvent);
        }

        private static void NonEmptyContext(SentryEvent sentryEvent)
        {
            Assert.That(sentryEvent.Contexts, Is.Not.Null);
            Assert.That(sentryEvent.Contexts.App, Is.Not.Null);
            Assert.That(sentryEvent.Contexts.OperatingSystem, Is.Not.Null);
            Assert.That(sentryEvent.Contexts.Device, Is.Not.Null);
            Assert.That(sentryEvent.Contexts.Runtime, Is.Not.Null);
        }
    }
}